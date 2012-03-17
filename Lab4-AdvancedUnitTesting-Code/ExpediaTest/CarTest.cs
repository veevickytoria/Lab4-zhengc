using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

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
		
		//Task 6
		[Test()]
		public void TestThatCarDoesGetCarLocationFromTheDatabase()
		{
			IDatabase mockDatabase = mocks.Stub<IDatabase>();
			int carNumber1 = 34;
			int carNumber2 = 55;
			String carLocation1 = "1397B";
			String carLocation2 = "1678D";
			int daysToRent = 10;
			
			using(mocks.Record())
			{
			  mockDatabase.getCarLocation(carNumber1);
			  LastCall.Return(carLocation1);

			  mockDatabase.getCarLocation(carNumber2);
			  LastCall.Return(carLocation2);
			}
			
			var target = new Car(daysToRent);
			target.Database = mockDatabase;
			
			String result;
			result = target.getCarLocation(carNumber2);
			Assert.AreEqual(carLocation2, result);
			
			result = target.getCarLocation(carNumber1);
			Assert.AreEqual(carLocation1,result);
		}
		
		//Task 7
		[Test()]
		public void TestThatCarDoesGetMileageFromDatabase()
		{
			IDatabase mockDatabase = mocks.Stub<IDatabase>();
			Int32 expectedMileage = 78645;

			mockDatabase.Miles = expectedMileage;
			
			//Task 9: Used the ObjectMother pattern
			var target = ObjectMother.BMW();
			target.Database = mockDatabase;
			Int32 actualMileage = target.Mileage;
			
			Assert.AreEqual(expectedMileage, actualMileage);
		}
	}
}
