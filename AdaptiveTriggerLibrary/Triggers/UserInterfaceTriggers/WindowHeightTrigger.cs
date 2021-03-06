﻿namespace AdaptiveTriggerLibrary.Triggers.UserInterfaceTriggers
{
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current window height
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    /// <remarks>Use this trigger as alternative to the <see cref="AdaptiveTrigger"/>.</remarks>
    public class WindowHeightTrigger : AdaptiveTriggerBase<double, IComparableModifier>,
                                       IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowHeightTrigger"/> class.
        /// Default modifier: <see cref="GreaterThanEqualToModifier"/>.
        /// </summary>
        public WindowHeightTrigger()
            : base(new GreaterThanEqualToModifier())
        {
            Window.Current.SizeChanged += MainWindow_SizeChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private double GetCurrentValue()
        {
            return Window.Current.Bounds.Height;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler

        private void MainWindow_SizeChanged(object sender, WindowSizeChangedEventArgs windowSizeChangedEventArgs)
        {
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IDynamicTrigger Members

        void IDynamicTrigger.ForceValidation()
        {
            CurrentValue = GetCurrentValue();
        }

        void IDynamicTrigger.SuspendUpdates()
        {
            Window.Current.SizeChanged -= MainWindow_SizeChanged;
        }

        void IDynamicTrigger.ResumeUpdates()
        {
            Window.Current.SizeChanged += MainWindow_SizeChanged;
        }

        #endregion
    }
}