using System;
using System.Windows.Forms;

namespace assignment
{
    // Interfaces for the dashboard actions
    public interface IViewSchedule
    {
        void Execute();
    }

    public interface IRequestCoaching
    {
        void Execute();
    }

    public interface IManageRequestsA
    {
        void Execute();
    }

    // Default implementations that show placeholder message boxes
    public class DefaultViewSchedule : IViewSchedule
    {
        public void Execute()
        {
            var f = new ViewScheduleForm();
            f.Show();
        }
    }

    public class DefaultRequestCoaching : IRequestCoaching
    {
        public void Execute()
        {
            var f = new RequestCoachingForm();
            f.Show();
        }
    }

    public class DefaultManageRequestsA : IManageRequestsA
    {
        public void Execute()
        {
            var f = new ManageRequestsFormA();
            f.Show();
        }
    }
}
