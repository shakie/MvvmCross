﻿// La.n.g.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Binders;
#if WINDOWS_PHONE
using System.Windows;
using Cirrious.MvvmCross.BindingEx.WindowsPhone;
#endif
#if NETFX_CORE
using Windows.UI.Xaml;
using Cirrious.MvvmCross.BindingEx.WindowsStore;
#endif

// ReSharper disable CheckNamespace
namespace mvx
// ReSharper restore CheckNamespace
{
// ReSharper disable InconsistentNaming
    public static class La
// ReSharper restore InconsistentNaming
    {
// ReSharper disable InconsistentNaming
        public static readonly DependencyProperty ngProperty =
// ReSharper restore InconsistentNaming
            DependencyProperty.RegisterAttached("ng",
                                                typeof (string),
                                                typeof (La),
                                                new PropertyMetadata(null, CallBackWhenngIsChanged));

        public static string Getng(DependencyObject obj)
        {
            return obj.GetValue(ngProperty) as string;
        }

        public static void Setng(
            DependencyObject obj,
            string value)
        {
            obj.SetValue(ngProperty, value);
        }

        private static IMvxBindingCreator _bindingCreator;
        private static IMvxBindingCreator BindingCreator
        {
            get
            {
                _bindingCreator = _bindingCreator ?? Mvx.Resolve<IMvxBindingCreator>();
                return _bindingCreator;
            }
        }

        private static void CallBackWhenngIsChanged(
            object sender,
            DependencyPropertyChangedEventArgs args)
        {
            BindingCreator.CreateBindings(sender, args, ParseBindingDescriptions);
        }

        private static IEnumerable<MvxBindingDescription> ParseBindingDescriptions(string languageText)
        {
            if (MvxSingleton<IMvxBindingSingletonCache>.Instance == null)
                return null;

            return MvxSingleton<IMvxBindingSingletonCache>.Instance.BindingDescriptionParser.LanguageParse(languageText);
        }
    }
}