using Api.Controllers;
using Data;
using Models.DataModels;

namespace test;

public class ReservationTest
{
    [Fact]
    public void MailShouldBeSent()
    {
        var reservationModel = new Reservation()
       {
            ID = 1,
            Date = new Schedule()
            {
                Date = "2021-10-01",
                Time = "12:00"
            },
            NumberOfPeople = 2,
            Tables = new List<Table>()
            {
                new()
                {
                    ID = 1,
                    Capacity = 2
                }
            },
            Email = "email",
            FirstName = "first",
            LastName = "last"
        };
        var fakeMailer = new FakeMailer();
        var dataManipulation = new DataManipulation();
        var reservation = new ReservationController(fakeMailer,dataManipulation);
        
        reservation.CreateReservation(reservationModel);
        
        Assert.True(fakeMailer.MailSent);
        Assert.Equal("email", fakeMailer.Email);
    }
}