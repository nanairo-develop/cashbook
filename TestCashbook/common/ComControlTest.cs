using Cashbook.common;

namespace TestsCashbook.common
{
    [TestClass()]
    public class ComControlTest
    {

        [TestMethod]
        public void SetErrorLabelColorTest()
        {
            // ���Ғl�ݒ�
            object expected = Color.Red;

            // �p�����[�^�ݒ�
            Label label = new();

            // �������s
            ComControl.SetErrorLabelColor(label);

            // �����l�ݒ�
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