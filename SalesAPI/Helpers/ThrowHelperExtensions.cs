//using System;
//using System.Threading.Tasks;

//namespace SalesAPI.Helpers
//{
//    public static class ThrowHelperExtensions
//    {
//        public static T ThrowIf<T>(this T obj, Func<T, bool> func, Exception ex)
//        {
//            if (func.Invoke(obj))
//            {
//                throw ex;
//            }

//            return obj;
//        }

//        public static T ThrowIfNull<T>(this T obj, Exception ex) where T : class
//        {
//            return obj.ThrowIf(result => result == null, ex);
//        }

//        public static async Task<T> ThrowIf<T>(this Task<T> obj, Func<T, bool> func, Func<T, Exception> exFunc)
//        {
//            await obj.ContinueWith(o =>
//            {
//                if (func.Invoke(o.Result))
//                {
//                    var ex = exFunc.Invoke(o.Result);

//                    throw ex;
//                }
//            });

//            return await obj;
//        }

//        public static async Task<T> ThrowIf<T>(this Task<T> obj, Func<T, bool> func, Exception ex)
//        {
//            return await obj.ThrowIf(func, _ => ex);
//        }

//        public static async Task<T> ThrowIfNull<T>(this Task<T> obj, Func<T, Exception> func) where T : class
//        {
//            return await obj.ThrowIf(result => result == null, e => func.Invoke(e));
//        }

//        public static async Task<T> ThrowIfNull<T>(this Task<T> obj, Exception ex) where T : class
//        {
//            return await obj.ThrowIfNull(_ => ex);
//        }

//    }
//}