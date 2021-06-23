using System.Windows;
using System.Windows.Controls.Primitives;

namespace Bewise.Phone
{
    public delegate void UpdateStateHandler(RatingButton sender, string state);
    public delegate void UpdateCheckedHandler(RatingButton sender);  

    [TemplateVisualState(Name = StateNormal, GroupName = GroupCommon)]
    [TemplateVisualState(Name = StateDisabled, GroupName = GroupCommon)]
    [TemplateVisualState(Name = StateUnchecked, GroupName = GroupCheck)]
    [TemplateVisualState(Name = StateChecked, GroupName = GroupCheck)]
    public class RatingButton: ToggleButton
    {
        internal const string GroupCommon = "CommonStates";
        internal const string GroupCheck = "CheckStates";

        internal const string StateDisabled = "Disabled";
        internal const string StateNormal = "Normal";
        internal const string StateUnchecked = "Unchecked";
        internal const string StateChecked = "Checked";

        int index;
        
        public event UpdateStateHandler UpdateState;
        public event UpdateCheckedHandler UpdateChecked;
        public RatingButton()
        {
            DefaultStyleKey = typeof(RatingButton);
            IsThreeState = true;
        }

        internal int Index
        {
            get { return index; }
            set { index = value; }
        }

        protected override void OnToggle()
        {
            bool? isChecked = IsChecked;
            if (isChecked == true)
            {
                IsChecked = false;
            }
            OnUpdateChecked();
        }

        protected void OnUpdateState(string state )
        {
            if (UpdateState != null)
            {
                UpdateState(this, state);
            }
        }
        protected void OnUpdateChecked()
        {
            if (UpdateChecked != null)
            {
                UpdateChecked(this);
            }
        }
    }
}
