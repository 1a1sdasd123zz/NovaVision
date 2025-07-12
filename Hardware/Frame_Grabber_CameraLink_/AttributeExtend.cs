using System;
using System.Reflection;
using NovaVision.BaseClass;

namespace NovaVision.Hardware.Frame_Grabber_CameraLink_
{
    public static class AttributeExtend
    {
        public static bool ExecuteMethod<T>(this T t, CameraParamChangeArgs e) where T : Bv_Camera
        {
            bool b_ret = false;
            Type type = t.GetType();
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (!method.IsDefined(typeof(SettingAttribute), inherit: true))
                {
                    continue;
                }
                try
                {
                    object[] customAttributes = method.GetCustomAttributes(typeof(SettingAttribute), inherit: true);
                    for (int j = 0; j < customAttributes.Length; j++)
                    {
                        SettingAttribute item = (SettingAttribute)customAttributes[j];
                        if (item.Discription.Equals(e.Name))
                        {
                            object @return = method.Invoke(t, new object[1] { e.NewValue });
                            b_ret = Convert.ToBoolean(@return);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.LogError("CameraLink相机特性" + e.Name + "赋值报错：" + ex.Message);
                    return b_ret;
                }
            }
            return b_ret;
        }
    }
}
