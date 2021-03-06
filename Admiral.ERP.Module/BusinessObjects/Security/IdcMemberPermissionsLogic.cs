﻿using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;

namespace Admiral.ERP.Module.BusinessObjects
{
    [DomainLogic(typeof(IDCMemberPermissions))]
    public class IDCMemberPermissionsLogic
    {
        public static bool? Get_EffectiveRead(IDCMemberPermissions memberPermissions)
        {
            bool? result;
            if (memberPermissions.AllowRead)
            {
                result = true;
            }
            else if (memberPermissions.Owner != null && memberPermissions.Owner.AllowRead)
            {
                result = null;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static void Set_EffectiveRead(IDCMemberPermissions memberPermissions, Boolean? value)
        {
            memberPermissions.AllowRead = value ?? false;
        }
        public static Boolean? Get_EffectiveWrite(IDCMemberPermissions memberPermissions)
        {
            Boolean? result;
            if (memberPermissions.AllowWrite)
            {
                result = true;
            }
            else if (memberPermissions.Owner != null && memberPermissions.Owner.AllowWrite)
            {
                result = null;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public static void Set_EffectiveWrite(IDCMemberPermissions memberPermissions, Boolean? value)
        {
            memberPermissions.AllowWrite = value ?? false;
        }
        public static String Get_InheritedFrom(IDCMemberPermissions memberPermissions)
        {
            String result = "";
            if (memberPermissions.Owner != null)
            {
                if (memberPermissions.Owner.AllowRead)
                {
                    result = String.Concat(result, String.Format(CaptionHelper.GetLocalizedText("Messages", "Read") + CaptionHelper.GetLocalizedText("Messages", "IsInheritedFrom"), CaptionHelper.GetClassCaption(memberPermissions.Owner.TargetType.FullName)));
                }
                if (memberPermissions.Owner.AllowWrite)
                {
                    result = String.Concat(result, String.Format(CaptionHelper.GetLocalizedText("Messages", "Write") + CaptionHelper.GetLocalizedText("Messages", "IsInheritedFrom"), CaptionHelper.GetClassCaption(memberPermissions.Owner.TargetType.FullName)));
                }
            }
            return result;
        }
        public static IList<IOperationPermission> GetPermissions(IDCMemberPermissions memberPermissions)
        {
            IList<IOperationPermission> result = new List<IOperationPermission>();
            if (memberPermissions.Owner != null && memberPermissions.Owner.TargetType != null && !String.IsNullOrEmpty(memberPermissions.Members))
            {
                if (memberPermissions.AllowRead)
                {
                    result.Add(new MemberOperationPermission(memberPermissions.Owner.TargetType, memberPermissions.Members, SecurityOperations.Read));
                }
                if (memberPermissions.AllowWrite)
                {
                    result.Add(new MemberOperationPermission(memberPermissions.Owner.TargetType, memberPermissions.Members, SecurityOperations.Write));
                }
            }
            return result;
        }
    }
}