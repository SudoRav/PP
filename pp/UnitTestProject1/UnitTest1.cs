using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using Microsoft.Analytics.UnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Login_from_1_1_TRUEreturned()
        {
            bool expected = true;
            bool result;

            // arrange
            string log = "1";
            string pas = "1";

            // act
            result = DBf.Login(log, pas, null);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Login_from_0_0_FALSEreturned()
        {
            bool expected = false;
            bool result;

            // arrange
            string log = "0";
            string pas = "0";

            // act
            result = DBf.Login(log, pas, null);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void validlog_from_1_FALSEreturned()
        {
            bool expected = false;
            bool result;

            // arrange
            string log = "1";

            // act
            result = DBf.validLog(log);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void validlog_from_0_TRUEreturned()
        {
            bool expected = true;
            bool result;

            // arrange
            string log = "0";

            // act
            result = DBf.validLog(log);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Register_from_2_2_TRUEreturned()
        {
            bool expected = true;
            bool result;

            // arrange
            string log = "2";
            string pas = "2";
            string F = "2";
            string I = "2";
            string O = "2";
            string mail = "2@gmail.com";
            string phone = "+7(222)222-22-22";
            string ier = "Сотрудник";

            // act
            result = DBf.Register(log, pas, F, I, O, mail, phone, ier);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Register_from_2_2_FALSEreturned()
        {
            bool expected = false;
            bool result;

            // arrange
            string log = "1";
            string pas = "1";
            string F = "1";
            string I = "1";
            string O = "1";
            string mail = "1@gmail.com";
            string phone = "";
            string ier = "Сотрудник";

            // act
            result = DBf.Register(log, pas, F, I, O, mail, phone, ier);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void recalculateTime_10min_to_11min_11returned()
        {
            string expected = "00:11:00";
            string result;

            // arrange
            string org_time = "00:10:00";
            string add_time = "00:01:00";

            // act
            result = DBf.recalculateTime(org_time, add_time);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void _updateTotalTime_0returned()
        {
            string expected = "00:00:00";
            string result;

            // arrange
            Stat.ID = "1";

            // act
            result = DBf.updateTotalTime();

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void delWorkers_TRUEreturned()
        {
            bool expected = true;
            bool result;

            // arrange
            Stat.ID = "2";

            // act
            result = DBf.delWorkers("2");

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}
