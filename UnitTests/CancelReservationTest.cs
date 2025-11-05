namespace UnitTests
{
    [TestClass]
    public sealed class Test2
    {
        [TestMethod]
        public void ReservationCancelTest()
        {
            // arrange
            var reservation = new ReservationModel(
                id: 1,
                time: DateTime.Now,
                numPeople: 2,
                remark: "Window seat",
                status: "Active",
                users_ID: 5
            );

            // act
            ReservationsLogic.CancelReservation(reservation);

            // assert
            Assert.AreEqual("Cancelled", reservation.Status);
        }
    }
}
