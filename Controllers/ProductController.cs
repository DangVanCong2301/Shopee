using Microsoft.AspNetCore.Mvc;
using Project.Models;

[Route("/product")]
public class ProductController : Controller {
    private readonly IProductResponsitory _productResponsitory;
    private readonly IHttpContextAccessor _accessor;
    private readonly ICartReponsitory _cartResponsitory;
    private readonly IHomeResponsitory _homeResponsitory;
    private readonly IUserResponsitory _userResponsitory;
    public ProductController(IProductResponsitory productResponsitory, ICartReponsitory cartReponsitoty, IHttpContextAccessor accessor, IHomeResponsitory homeResponsitory, IUserResponsitory userResponsitory)
    {
        _productResponsitory = productResponsitory;
        _cartResponsitory = cartReponsitoty;
        _accessor = accessor;
        _homeResponsitory = homeResponsitory;
        _userResponsitory = userResponsitory;
    }

    [Route("index/{categoryID}")]
    public IActionResult Index(int categoryID, int currentPage = 1) {
        IEnumerable<Product> products;
        var userID = _accessor?.HttpContext?.Session.GetInt32("UserID");
        System.Console.WriteLine("UserID: " + userID);
        List<User> users = _userResponsitory.checkUserLogin(Convert.ToInt32(userID)).ToList();
        if (users.Count() == 0) {
            products = _productResponsitory.getProductsByCategoryID(categoryID).ToList();
        } else if (users[0].FK_iRoleID == 2) {
            products = _productResponsitory.getProductsByCategoryIDIfRoleAdmin(categoryID).ToList();
        } else {
            products = _productResponsitory.getProductsByCategoryID(categoryID).ToList();
        }
        int totalRecord = products.Count();
        int pageSize = 12;
        int totalPage = (int) Math.Ceiling(totalRecord / (double) pageSize);
        products = products.Skip((currentPage - 1) * pageSize).Take(pageSize);
        IEnumerable<CartDetail> cartDetails = _cartResponsitory.getCartInfo(Convert.ToInt32(userID)).ToList();
        IEnumerable<Category> categories = _homeResponsitory.getCategories().ToList();
        // Vì mình lấy layout của _Layout của kiểu là @model ProducdViewModel nó sẽ chung cho tất cả các trang, ta lấy riêng nó sẽ lỗi
        ShopeeViewModel model = new ShopeeViewModel {
            Products = products,
            Categories = categories,
            CartDetails = cartDetails,
            CurrentCategoryID = categoryID,
            TotalPage = totalPage,
            PageSize = pageSize,
            CurrentPage = currentPage,
            CartCount = cartDetails.Count()
        };
        return View(model);
    }

    /// <summary>
    /// Nguồn: https://xuanthulab.net/asp-net-core-mvc-chi-tiet-ve-route-trong-asp-net-mvc.html
    /// </summary>
    [Route("detail/{id?}")]
    public IActionResult Detail(int id)
    {
        _accessor?.HttpContext?.Session.SetInt32("CurrentProductID", id);
        var sessionUserID = _accessor?.HttpContext?.Session.GetInt32("UserID");
        IEnumerable<Store> stores = _homeResponsitory.getStores();
        IEnumerable<Product> product = _productResponsitory.getProductByID(id);
        IEnumerable<Favorite> favorites = _homeResponsitory.getFavorites(Convert.ToInt32(sessionUserID));
        IEnumerable<CartDetail> cartDetails = _cartResponsitory.getCartInfo(Convert.ToInt32(sessionUserID)).ToList();
        ShopeeViewModel model = new ShopeeViewModel {
            Stores = stores,
            Products = product,
            Favorites = favorites,
            CartDetails = cartDetails,
            CartCount = cartDetails.Count(),
            CurrentProductID = id
        };
        return View(model);
    }

    [HttpPost]
    [Route("/product/get-data-detail")]
    public IActionResult GetDetail() {
        var sessionCurrentProductID = _accessor?.HttpContext?.Session.GetInt32("CurrentProductID");
        var product = _productResponsitory.getProductByID(Convert.ToInt32(sessionCurrentProductID));
        ProductViewModel model = new ProductViewModel {
            Products = product
        };
        return Ok(model);
    }

    [Route("sort/{categoryID?}/{sortType?}")]
    public IActionResult Sort(int categoryID, string sortType = "") {
        IEnumerable<Product> products;
        var userID = _accessor?.HttpContext?.Session.GetInt32("UserID");
        if (sortType == "asc") {
            products = _productResponsitory.getProductsByCategoryIDAndSortIncre(categoryID); // Gọi đúng phương thức sắp xếp tăng dần nhé
        } else {
            products = _productResponsitory.getProductsByCategoryIDAndSortReduce(categoryID); // Gọi đúng phương thức sắp xếp giảm dần nhé
        }
        IEnumerable<CartDetail> cartDetails = _cartResponsitory.getCartInfo(Convert.ToInt32(userID));
        IEnumerable<Category> categories = _homeResponsitory.getCategories();
        ProductViewModel model = new ProductViewModel {
            Products = products,
            CartDetails = cartDetails,
            Categories = categories,
            CurrentCategoryID = categoryID
        };
        //return Json(model);
        return View("Index", model);
    }

    [Route("/Home/CartDetail")]
    public IActionResult CartDetail()
    {
        return View();
    }
}