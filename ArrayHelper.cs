using System;
using System.Collections.Generic;


/// <summary>
/// 数组助手类(工具类)
/// </summary>
public static class ArrayHelper
{
    /// <summary>
    /// 用于查找时的比较条件委托
    /// </summary>
    /// <param name="obj">要比较的对象</param>
    /// <returns>比较的结束</returns>
    public delegate bool FindHandler<T>(T obj);

    /// <summary>
    /// 用T类型对象中，提取出TKey类型的结果
    /// </summary>
    /// <param name="obj">源对象</param>
    /// <returns>从源对象中提取出的结果</returns>
    public delegate TKey SelectHandler<T, TKey>(T obj);

    /// <summary>
    /// 查找满足条件的单个对象
    /// </summary>
    /// <param name="array">源数组</param>
    /// <param name="handler">匹配条件</param>
    /// <returns>查找到的结果</returns>
    public static T Find<T>(T[] array, FindHandler<T> handler)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (handler(array[i]))
                return array[i];
        }
        return default(T);
    }

    /// <summary>
    /// 查找满足条件的所有对象
    /// </summary>
    /// <param name="array">源数组</param>
    /// <param name="handler">匹配条件</param>
    /// <returns>查找到的结果</returns>
    public static T[] FindAll<T>(T[] array, FindHandler<T> handler)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < array.Length; i++)
        {
            if (handler(array[i]))
                list.Add(array[i]);
        }
        return list.Count > 0 ? list.ToArray() : null;
    }

    /// <summary>
    /// 按照比较条件从数组中找出最大的元素
    /// </summary>
    /// <typeparam name="T">对象数据类型</typeparam>
    /// <typeparam name="TKey">比较大小的关键数据的类型</typeparam>
    /// <param name="array">源数组</param>
    /// <param name="handler">提取比较关键数据的委托</param>
    /// <returns>按比较关键字找出的最大对象</returns>
    public static T Max<T, TKey>(T[] array, SelectHandler<T, TKey> handler)
        where TKey : IComparable, IComparable<TKey>
    {
        T max = array[0];
        for (int i = 0; i < array.Length; i++)
        {
            if (handler(array[i]).CompareTo(handler(max)) > 0)
                max = array[i];
        }
        return max;
    }

    /// <summary>
    /// 按照比较条件从数组中找出最小的元素
    /// </summary>
    /// <typeparam name="T">对象数据类型</typeparam>
    /// <typeparam name="TKey">比较大小的关键数据的类型</typeparam>
    /// <param name="array">源数组</param>
    /// <param name="handler">提取比较关键数据的委托</param>
    /// <returns>按比较关键字找出的最小对象</returns>
    public static T Min<T, TKey>(T[] array, SelectHandler<T, TKey> handler)
       where TKey : IComparable, IComparable<TKey>
    {
        T min = array[0];
        for (int i = 0; i < array.Length; i++)
        {
            if (handler(array[i]).CompareTo(handler(min)) < 0)
                min = array[i];
        }
        return min;
    }

    /// <summary>
    /// 升序排列
    /// </summary>
    /// <param name="array">源数组</param>
    /// <param name="handler">排序依据的关键词</param>
    public static void OrderBy<T,TKey>(T[] array,SelectHandler<T, TKey> handler)
        where TKey:IComparable,IComparable<TKey>
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (handler(array[i]).CompareTo(handler(array[j])) > 0)
                {
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
    }

    /// <summary>
    /// 降序排列
    /// </summary>
    /// <param name="array">源数组</param>
    /// <param name="handler">排序依据的关键词</param>
    public static void OrderByDescending<T, TKey>(T[] array, SelectHandler<T, TKey> handler)
        where TKey : IComparable, IComparable<TKey>
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (handler(array[i]).CompareTo(handler(array[j])) < 0)
                {
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
    }


    /// <summary>
    /// 选择提取数组中部分信息后形成一个新的数组
    /// </summary>
    /// <param name="array">源数组</param>
    /// <param name="handler">提取算法</param>
    /// <returns>提取后的结果</returns>
    public static TKey[] Select<T, TKey>(T[] array, SelectHandler<T, TKey> handler)
    {
        TKey[] tempArr = new TKey[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            tempArr[i] = handler(array[i]);
        }
        return tempArr;
    }
}

