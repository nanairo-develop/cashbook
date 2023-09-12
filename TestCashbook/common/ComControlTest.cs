using cashbook.common;

namespace TestCashbook.common
{
    [TestClass]
    public class ComCotrolTest
    {
        [TestMethod]
        public void TestSetErrorLabelColor()
        {
            // 期待値設定
            object e = Color.Red;

            // パラメータ設定
            Label label = new();

            // 処理実行
            ComControl.SetErrorLabelColor(label);

            // 実効値設定
            object a = label.BackColor;

            Assert.AreEqual(e,a);
        }
    }
}