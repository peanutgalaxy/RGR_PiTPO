using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using WindowsFormsApp1;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Form1 form = new Form1();
            string text1 = "5";
            string text2 = "fknshlfkn";
            //string text3 
            int var = form.Begin2(text1, text2);
            Assert.AreEqual(var, 3);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Form1 form = new Form1();
            string text1 = "5";
            string text2 = "C:\\Users\\Мария\\Pictures\\картинки\\волшебство.jpg";
            //string text3 
            int var = form.Begin2(text1, text2);
            Assert.AreEqual(var, 1);
        }
        [TestMethod]
        public void TestMethod3()
        {
            Form1 form = new Form1();
            string text1 = "15";
            string text2 = "C:\\Users\\Мария\\Pictures\\картинки\\волшебство.jpg";
            //string text3 
            int var = form.Begin2(text1, text2);
            Assert.AreEqual(var, 3);
        }
        [TestMethod]
        public void TestMethod4()
        {
            Form1 form = new Form1();
            string text1 = "C:\\Users\\Мария\\Pictures\\картинки\\волшебство.jpg";
            string text2 = "9";
            //string text3 
            int var = form.Begin2(text1, text2);
            Assert.AreEqual(var, 3);
        }
    }
}
