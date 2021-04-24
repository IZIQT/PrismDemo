using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrismDemo.Model;
using PrismDemo.ViewModels;
using System;

namespace MyUnitiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MainWindowViewModel mw = new MainWindowViewModel();
            mw.NewTabCommand.Execute(null);
            Assert.AreEqual(2, mw.TabViewModels.Count, "Не верно сработало добавление строчки!");
        }
    }
}
