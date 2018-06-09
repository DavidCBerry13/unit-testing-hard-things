using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAppForPose
{

    public class RandomService
    {
        public RandomService()
        {
            logger = LogManager.GetLogger(this.GetType());
        }


        private ILog logger;




        public void ReallyUsefulMethod()
        {
            logger.Info("Method Called");


        }



    }
}
