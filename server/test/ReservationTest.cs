using Api.Controllers;
using Data;
using Models.DataModels;

namespace test;

public class ReservationTest
{
    private readonly Reservation _reservationModel =  new()
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
    
    [Fact]
    public void MailShouldBeSent()
    {
        var fakeMailer = new FakeMailer();
        var dataManipulation = new FakeDataManipulation();
        var reservation = new ReservationController(fakeMailer,dataManipulation);
        
        reservation.CreateReservation(_reservationModel);
        
        Assert.True(fakeMailer.MailSent);
        Assert.Equal("email", fakeMailer.Email);
    }
    [Fact]
    public void ReservationShouldBeAdded()
    {
        var fakeMailer = new FakeMailer();
        var dataManipulation = new FakeDataManipulation();
        var reservation = new ReservationController(fakeMailer,dataManipulation);
        
        reservation.CreateReservation(_reservationModel);
        
        Assert.Equal(_reservationModel, dataManipulation.LastReservation);
    }
}