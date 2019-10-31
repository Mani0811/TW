using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ConferenceTrackManagement.UnitTest
{

    [TestClass]
   public class ConferenceSchedulerTest
    {
        [TestMethod]
        public void ParseInputLine_No_error()
        {
            Event event_obj = ConferenceScheduler.ParseInputLine("Sit Down and Write 30min");
            Assert.AreNotEqual(event_obj, null);
            Assert.AreEqual(event_obj.Name, "Sit Down and Write");
            Assert.AreEqual(event_obj.GetDurationInMinutes(),30);
        }
    }
}
