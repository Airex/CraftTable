using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using CraftTable.Contracts;
using CraftTable.Exceptions;
using Moq;
using NUnit.Framework;

namespace CraftTable.Tests
{
    public class CraftTableTests
    {
        [Test]
        public void AbilityNotAvailableTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            Mock<Ability> abilityMock = new Mock<Ability>();
            abilityMock.Setup(ability => ability.CanAct(It.IsAny<ICraftServiceState>())).Returns(false);

            Assert.Throws<AbilityNotAvailableException>(() => craftTable.Act(abilityMock.Object));

        }

        [Test]
        public void CraftFailedTest()
        {
            var craftTable = TestData.CreateFactory()(new Recipe(1000, 30, 1000), TestData.DefaultCraftman);

            Assert.Throws<CraftFailedException>(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    craftTable.Act(new StubAbility());
                }
            });

        }


        [Test]
        public void CraftSuccessTest()
        {
            var craftTable = TestData.CreateFactory()(new Recipe(40, 100, 100), TestData.DefaultCraftman);
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    craftTable.Act(new StubAbility());
                }
                Assert.Fail();
            }
            catch (CraftSuccessException)
            {
                
            }
        }

        [Test]
        public void AllbilitiesTest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new RegistrationModule());
            var container = builder.Build();

            var enumerable = container.Resolve<IEnumerable<Ability>>();
            Assert.That(enumerable, Is.Not.Null);
            Assert.That(enumerable.Count()>=10);
            foreach (var ability in enumerable)
            {
                Console.WriteLine(ability);
            }
        }

        private class StubAbility : Ability
        {
            public override void Execute(ICraftActions craftActions)
            {
                craftActions.UseDurability(10);
                craftActions.Synth(Synth.FromRawValue(20));
                craftActions.Touch(100);
            }

            public override bool CanAct(ICraftServiceState serviceState)
            {
                return true;
            }
        }
    }
}