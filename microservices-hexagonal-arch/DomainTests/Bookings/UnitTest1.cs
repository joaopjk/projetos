using Domain.Entities;
using Domain.Enums;
using Action = Domain.Enums.Action;

namespace DomainTests.Bookings
{
    public class StateMachineTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();

            Assert.That(booking.CurrentStatus == Status.Created);
        }

        [Test]
        public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);

            Assert.That(booking.CurrentStatus == Status.Paid);
        }

        [Test]
        public void ShouldSetStatusToCanceledWhenCancelingForABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Cancel);

            Assert.That(booking.CurrentStatus == Status.Canceled);
        }

        [Test]
        public void ShouldSetStatusToFinishedWhenFinishingForABookingWithPaidStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);

            Assert.That(booking.CurrentStatus == Status.Finished);
        }

        [Test]
        public void ShouldSetStatusToRefoundWhenRefoundForABookingWithPaidStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Refound);

            Assert.That(booking.CurrentStatus == Status.Refound);
        }

        [Test]
        public void ShouldSetStatusToCreatedWhenReopeningForABookingWithCanceledStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Action.Cancel);
            booking.ChangeState(Action.Reopen);
            Assert.That(booking.CurrentStatus == Status.Created);
        }
    }
}