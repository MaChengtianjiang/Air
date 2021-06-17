using System.IO;
using System;
using UnityEngine;

public class ScriptCreateInit : UnityEditor.AssetModificationProcessor
{
    private static string fileDescribe =
        "/****************************************************\n" +
        "* Unity版本：#UnityVersion#\n" +
        "* 文件：#SCRIPTNAME#\n" +
        "* 作者：#CreateAuthor#\n" +
        "* 邮箱：#Email#\n" +
        "* 日期：#CreateTime#\n" +
        "* 功能：Nothing\n" +
        "*****************************************************/\n\n";

    private static string author = "tottimctj";//Environment.UserName
    private static string email = "tottimctj@163.com";
    /// <summary>
    /// 在创建资源的时候执行的函数
    /// </summary>
    /// <param name="path">路径</param>
    private static void OnWillCreateAsset(string path)
    {
        
        //将.meta后缀屏蔽，避免获取到的是新建脚本时候的 meta 文件
        path = path.Replace(".meta", "");

        //只对.cs脚本作操作
        if (path.EndsWith(".cs"))
        {

            // 文件名的分割获取
            string[] iterm = path.Split('/');

            string str = fileDescribe;

            //读取该路径下的.cs文件中的所有文本.
            str += File.ReadAllText(path);

            // 进行关键字的文件名、作者和时间获取，并替换
            str = str.Replace("#SCRIPTNAME#", iterm[iterm.Length - 1])
                .Replace("#CreateAuthor#", author)
                .Replace("#Email#", email)
                .Replace("#CreateTime#", string.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}", DateTime.Now.Year,
                DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))
                .Replace("#UnityVersion#", Application.unityVersion)
            .Replace(Environment.NewLine+ "    // Start is called before the first frame update", "")
            .Replace(Environment.NewLine+"    // Update is called once per frame", "");
            // 重新写入脚本中，完成数据修改
            File.WriteAllText(path, str);
        }
    }
}