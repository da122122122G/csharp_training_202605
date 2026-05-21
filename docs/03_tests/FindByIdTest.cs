using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading.Tasks;
using WebApp_training.Infrastructures.Repositories;



namespace _03_tests
{
    public class FindByIdTest
    {
        /*    [TestMethod("テストケース1:指定の部署があった")]
            public void FindById_Success()
            {
                int id = 2;

                var result = FindById(id);
                Assert.AreEqual(2, result.Count);
                ollectionAssert.Contains(result, "総務部");
            }*/

        [TestMethod("テストケース2:指定の部署がなかった")]
        public void FindById_Failuar()
        {
            var departmentRepository = new DepartmentRepository();
            int id = 10;

            var result = FindById(id);
            Assert.IsNull(result);
        }

    }
}