using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestThatCarDoesGetCarLocationFromTheDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            String carLocation = "Faculty Lot";
            String anotherCarLocation = "Vistor's Lot";

            using(mocks.Record())
            {
                mockDatabase.getCarLocation(32);
                LastCall.Return(carLocation);

                mockDatabase.getCarLocation(1025);
                LastCall.Return(anotherCarLocation);
            }

            var target = new Car(10);
            target.Database = mockDatabase;

            String result;

            result = target.getCarLocation(1025);
            Assert.AreEqual(result, anotherCarLocation);

            result = target.getCarLocation(32);
            Assert.AreEqual(result, carLocation);
        }

        [Test()]
        public void TestThatCarDoesGetMilageFromDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();

            int miles = 32;
            mockDatabase.Miles = miles;

            var target = new Car(10);
            target.Database = mockDatabase;

            int mileage = target.Mileage;
            Assert.AreEqual(mileage, miles);
        }
	}
}
