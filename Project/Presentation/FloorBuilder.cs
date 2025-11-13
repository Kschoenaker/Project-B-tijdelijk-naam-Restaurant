public enum FloorTableStatus
{
    None, // default / empty
    Reserved, // already booked
    UnAvailable, // cannot select
    HoveredOn, // cursor is on this table
    Selected // when selected
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
    public List<FloorTable> UnreservedFloorTables;
    public List<FloorTable> AvailableFloorTables;
    public List<FloorTable> SelectedTables;
    public FloorTable? CurrentHoveredTable;

    private const int PaddingX = 4;
    private const int PaddingY = 2;
    private const int MaxRowWidth = 50;

    public FloorBuilder(List<TablesModel> allTables, List<TablesModel> unreservedTables, List<TablesModel> availableTables)
    {
        AllFloorTables = GenerateMap(allTables, availableTables);
        AvailableFloorTables = AllFloorTables
            .Where(ft => availableTables.Any(t => t.ID == ft.Table.ID))
            .ToList();

        SelectedTables = new();
        UnreservedFloorTables = new();

        foreach (var floorTable in AllFloorTables)
        {
            if (unreservedTables.Any(t => t.ID == floorTable.Table.ID))
            {
                UnreservedFloorTables.Add(floorTable);
            }
        }

        // Start on first available table
        CurrentHoveredTable = AvailableFloorTables.FirstOrDefault();
        if (CurrentHoveredTable != null)
            CurrentHoveredTable.Status = FloorTableStatus.HoveredOn;
    }

    private List<FloorTable> GenerateMap(List<TablesModel> tables, List<TablesModel> availableTables)
    {
        List<FloorTable> floorTables = new();
        int currentX = 0, currentY = 0, rowHeight = 0;

        foreach (var table in tables)
        {
            int width = Math.Clamp(table.TableSeats / 2, 1, 8) * 3;
            int height = 3; // fixed height for display simplicity

            if (currentX + width > MaxRowWidth)
            {
                currentX = 0;
                currentY += rowHeight + PaddingY;
                rowHeight = 0;
            }

            FloorTable ft = new FloorTable(currentX, currentY, width, height, table);

            bool isAvailable = availableTables.Any(t => t.ID == table.ID);
            ft.Status = isAvailable ? FloorTableStatus.None : FloorTableStatus.UnAvailable;


            floorTables.Add(ft);
            currentX += width + PaddingX;
            rowHeight = Math.Max(rowHeight, height);
        }

        return floorTables;
    }

    // Move to the nearest table in the requested direction
    public bool MoveSelection(int deltaX, int deltaY)
    {
        if (CurrentHoveredTable == null)
            return false;

        // Get all candidates in that direction
        var candidates = AllFloorTables.Where(ft =>
        {
            if (deltaX > 0) return ft.X > CurrentHoveredTable.X + CurrentHoveredTable.Width - 1;
            if (deltaX < 0) return ft.X + ft.Width - 1 < CurrentHoveredTable.X;
            if (deltaY > 0) return ft.Y > CurrentHoveredTable.Y + CurrentHoveredTable.Height - 1;
            if (deltaY < 0) return ft.Y + ft.Height - 1 < CurrentHoveredTable.Y;
            return false;
        });

        // Pick the closest table that overlaps on the perpendicular axis
        FloorTable? target = null;
        if (deltaX != 0)
        {
            target = candidates
                .Where(ft => ft.Y < CurrentHoveredTable.Y + CurrentHoveredTable.Height &&
                             ft.Y + ft.Height > CurrentHoveredTable.Y)
                .OrderBy(ft => Math.Abs(ft.X - CurrentHoveredTable.X))
                .FirstOrDefault();
        }
        else if (deltaY != 0)
        {
            target = candidates
                .Where(ft => ft.X < CurrentHoveredTable.X + CurrentHoveredTable.Width &&
                             ft.X + ft.Width > CurrentHoveredTable.X)
                .OrderBy(ft => Math.Abs(ft.Y - CurrentHoveredTable.Y))
                .FirstOrDefault();
        }

        if (target == null)
            return false; // no table found in that direction

        // Reset previous hovered status
        if (CurrentHoveredTable.Status == FloorTableStatus.HoveredOn)
        {
            if (AvailableFloorTables.Contains(CurrentHoveredTable))
            {
                CurrentHoveredTable.Status = SelectedTables.Contains(CurrentHoveredTable)
                    ? FloorTableStatus.Selected
                    : FloorTableStatus.None;
            }
            else
            {
                CurrentHoveredTable.Status = FloorTableStatus.UnAvailable;
            }
        }

        // Set new hovered table
        CurrentHoveredTable = target;
        if (!SelectedTables.Contains(target))
        {
            if (target.Status != FloorTableStatus.Reserved && target.Status != FloorTableStatus.Selected)
            {
                target.Status = FloorTableStatus.HoveredOn;
            }
        }
        else
        {
            target.Status = FloorTableStatus.Selected;
        }

        return true;
    }

    public bool SelectCurrent()
    {
        if (CurrentHoveredTable == null || !AvailableFloorTables.Contains(CurrentHoveredTable))
            return false;

        CurrentHoveredTable.Status = FloorTableStatus.Selected;
        SelectedTables.Add(CurrentHoveredTable);
        return true;
    }

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
                    FloorTable ft = table;
                    if (!UnreservedFloorTables.Contains(ft))
                    {
                        ft.Status = FloorTableStatus.Reserved;
                    }

                    if (SelectedTables.Contains(ft))
                    {
                        ft.Status = FloorTableStatus.Selected;
                    }

                    switch (ft.Status)
                    {
                        case FloorTableStatus.HoveredOn:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case FloorTableStatus.Selected:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case FloorTableStatus.UnAvailable:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case FloorTableStatus.Reserved:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            Console.ResetColor();
                            break;
                    }
                    Console.Write("█");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }

        // ℹ️ Info box for the hovered table
        if (CurrentHoveredTable != null)
        {
            Console.WriteLine();
            Console.WriteLine("═══════════════════════════════════════════");
            Console.WriteLine($"Table:   {CurrentHoveredTable.Table.TablesName}");
            Console.WriteLine($"Seats:   {CurrentHoveredTable.Table.TableSeats}");
            Console.WriteLine(!AvailableFloorTables.Contains(CurrentHoveredTable) ? "This table can't be selected." : "Press ENTER to select this table.");
        }
    }
}

