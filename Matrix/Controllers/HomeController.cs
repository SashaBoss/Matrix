namespace Matrix.Controllers
{
    using Matrix.Interfaces;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private IMatrixService _matrixService;

        public HomeController(IMatrixService matrixService)
        {
            this._matrixService = matrixService;
        }

        public ActionResult Index()
        {
            var randomMatrix = this._matrixService.GenerateRandom(5);

            return View(randomMatrix);
        }

        public ActionResult GenerateRandom(int size)
        {
            return View("Index", this._matrixService.GenerateRandom(size));
        }

        public ActionResult ExportData(string flattened, int size)
        {
            int[,] twoDArray = this._matrixService.FromFlattenedString(flattened, size);

            var enumerator = twoDArray.Cast<int>()
                            .Select((s, i) => (i + 1) % size == 0 ? string.Concat(s, Environment.NewLine) : string.Concat(s, ","));

            var item = string.Join("", enumerator.ToArray());

            return File(new System.Text.UTF8Encoding().GetBytes(item), "text/csv", "matrix.csv");
        }

        public ActionResult Transpose(string flattened, int size)
        {
            int[,] twoDArray = this._matrixService.FromFlattenedString(flattened, size);
            var transposed = this._matrixService.TransposeInPlace(twoDArray, size);

            return View("Index", transposed);
        }

        [HttpPost]
        public ActionResult Load(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;

            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);
                var vm = this._matrixService.ReadFromFile(filePath);

                return View("Index", vm);
            }

            return View("Index");
        }
    }
}