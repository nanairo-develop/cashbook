using Cashbook.common;

namespace TestsCashbook.common
{
    [TestClass()]
    public class ComControlTest
    {

        [TestMethod]
        public void SetErrorLabelColorTest()
        {
            // 期待値設定
            object expected = Color.Red;

            // パラメータ設定
            Label label = new();

            // 処理実行
            ComControl.SetErrorLabelColor(label);

            // 実効値設定
            object actual = label.BackColor;

            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void SetClearLabelColorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetClearColorTest()
        {

        }
    }
}