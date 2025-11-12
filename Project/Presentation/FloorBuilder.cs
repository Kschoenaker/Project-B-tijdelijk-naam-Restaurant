public enum FloorTableStatus
{
    None, // default / empty
    Reserved, // already booked
    UnAvailable, // cannot select
    HoveredOn, // cursor is on this table
    Selected
}

public class FloorTable
{
    public int X;
    public int Y;
    public int Width;
    public int Height;
    public TablesModel Table;
    public FloorTableStatus Status;

    public FloorTable(int x, int y, int width, int height, TablesModel table)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        Table = table;
        Status = FloorTableStatus.None;
    }

    // Check if a given coordinate is inside this table
    public bool Contains(int cursorX, int cursorY)
    {
        return cursorX >= X && cursorX < X + Width &&
               cursorY >= Y && cursorY < Y + Height;
    }
}

public class FloorBuilder
{
    public List<FloorTable> AllFloorTables;
    public List<FloorTable> AvailableFloorTables;
    public List<FloorTable> SelectedTables;

    private const int PaddingX = 2; // space between tables horizontally
    private const int PaddingY = 1; // space between tables vertically

    public int CursorX { get; set; }
    public int CursorY { get; set; }

    public FloorBuilder(List<TablesModel> allTables, List<TablesModel> availableTables)
    {
        // Convert tables into FloorTable objects with positions & sizes
        AllFloorTables = GenerateMap(allTables, availableTables);
        AvailableFloorTables = AllFloorTables.Where(ft => availableTables.Contains(ft.Table)).ToList();
        SelectedTables = new();

        // Initialize cursor at first available table
        var first = AvailableFloorTables.FirstOrDefault();
        if (first != null)
        {
            CursorX = first.X;
            CursorY = first.Y;
            first.Status = FloorTableStatus.HoveredOn;
        }
    }

        private List<FloorTable> GenerateMap(List<TablesModel> tables, List<TablesModel> availableTables)
    {
        List<FloorTable> floorTables = new();
        int currentX = 0;
        int currentY = 0;
        int rowHeight = 0;
        int maxRowWidth = 50; // max width per row before wrapping

        foreach (TablesModel table in tables)
        {
            int width = 0;  // table width in characters
            int height = 0; // table height in characters

            switch (table.TableSeats)
            {
                case 2:
                    width = 3;
                    height = 2;
                    break;
                case 4:
                    width = 5;
                    height = 4;
                    break;
                case 6:
                    width = 6;
                    height = 5;
                    break;
            }

            // Wrap to next row if exceeded max width
            if (currentX + width > maxRowWidth)
            {
                currentX = 0;
                currentY += rowHeight + PaddingY;
                rowHeight = 0;
            }

            FloorTable floorTable = new FloorTable(currentX, currentY, width, height, table);

            if (!availableTables.Contains(table))
            {
                floorTable.Status = FloorTableStatus.UnAvailable;
            }
            
            floorTables.Add(floorTable);

            currentX += width + PaddingX;         // move cursor to the right for next table
            rowHeight = Math.Max(rowHeight, height); // track tallest table in current row
        }

        return floorTables;
    }

    // Move the cursor and update hovered status
    public void MoveCursor(int deltaX, int deltaY)
    {
        int newX = CursorX + deltaX;
        int newY = CursorY + deltaY;

        // Only move if the new position is inside the grid and not reserved/unavailable
        var hovered = AllFloorTables.FirstOrDefault(ft => ft.Contains(newX, newY));
        if (hovered != null)
        {
            // Reset previous hovered table
            var prevHovered = AllFloorTables.FirstOrDefault(ft => ft.Status == FloorTableStatus.HoveredOn);
            if (prevHovered != null && prevHovered.Status != FloorTableStatus.Selected)
                prevHovered.Status = AvailableFloorTables.Contains(prevHovered) ? FloorTableStatus.None : FloorTableStatus.UnAvailable;

            // Update new hovered table
            if (!SelectedTables.Contains(hovered))
                hovered.Status = FloorTableStatus.HoveredOn;

            CursorX = newX;
            CursorY = newY;
        }
    }

    // Select the currently hovered table
    public bool SelectHoveredTable()
    {
        var hovered = AllFloorTables.FirstOrDefault(ft => ft.Status == FloorTableStatus.HoveredOn);
        if (hovered != null && AvailableFloorTables.Contains(hovered))
        {
            SelectedTables.Add(hovered);
            hovered.Status = FloorTableStatus.Reserved; // mark as taken
            return true;
        }
        return false;
    }

    // Draw the floor to console
    public void DrawFloor()
    {
        int maxX = AllFloorTables.Max(ft => ft.X + ft.Width);
        int maxY = AllFloorTables.Max(ft => ft.Y + ft.Height);

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                var table = AllFloorTables.FirstOrDefault(ft => ft.Contains(x, y));

                if (table != null)
                {
                    switch (table.Status)
                    {
                        case FloorTableStatus.HoveredOn:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case FloorTableStatus.Reserved:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case FloorTableStatus.UnAvailable:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        default:
                            Console.ResetColor();
                            break;
                    }

                    Console.Write("█"); // each cell of table
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(" "); // empty space
                }
            }
            Console.WriteLine();
        }
    }
}
