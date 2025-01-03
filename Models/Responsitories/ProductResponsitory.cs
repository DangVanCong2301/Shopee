using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Domain;

public class ProductResponsitory : IProductResponsitory {
    private readonly DatabaseContext _context;
    public ProductResponsitory(DatabaseContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> getProductsByCategoryID(int categoryID)
    {
        SqlParameter categoryIDParam = new SqlParameter("@FK_iCategoryID", categoryID);
        return _context.Products.FromSqlRaw("EXEC sp_SelectProductsByCategoryID @FK_iCategoryID", categoryIDParam);
    }

    public IEnumerable<Product> getProductByID(int productID) {
        SqlParameter productIDParam = new SqlParameter("@PK_iProductID", productID);
        return _context.Products.FromSqlRaw("EXEC sp_SelectProductByID @PK_iProductID", productIDParam);
    }

    public IEnumerable<Product> getProductsByCategoryIDAndSortIncre(int categoryID)
    {
        SqlParameter categoryIDParam = new SqlParameter("@FK_iCategoryID", categoryID);
        return _context.Products.FromSqlRaw("sp_SelectProductsByCategoryIDAndSortIncre @FK_iCategoryID", categoryIDParam);
    }

    public IEnumerable<Product> getProductsByCategoryIDAndSortReduce(int categoryID)
    {
        SqlParameter categoryIDParam = new SqlParameter("@FK_iCategoryID", categoryID);
        return _context.Products.FromSqlRaw("sp_SelectProductsByCategoryIDAndSortReduce @FK_iCategoryID", categoryIDParam); // Đúng tên thủ tục lưu sắp xếp giảm dần
    }

    public IEnumerable<Product> getProductsByCategoryIDIfRoleAdmin(int categoryID)
    {
        SqlParameter categoryIDParam = new SqlParameter("@FK_iCategoryID", categoryID);
        return _context.Products.FromSqlRaw("sp_SelectProductsByCategoryIDIfRoleAdmin @FK_iCategoryID", categoryIDParam);
    }

    public IEnumerable<Product> getProductsByParentCategoryID(int parentCategoryID)
    {
        SqlParameter parentCategoryParam = new SqlParameter("@FK_iParentCategoryID", parentCategoryID);
        return _context.Products.FromSqlRaw("EXEC sp_SelectProductsByParentCategoryID @FK_iParentCategoryID", parentCategoryParam);
    }

    public IEnumerable<Product> getProductsByParentCategoryIDIfRoleAdmin(int parentCategoryID)
    {
        SqlParameter parentCategoryParam = new SqlParameter("@FK_iParentCategoryID", parentCategoryID);
        return _context.Products.FromSqlRaw("EXEC sp_SelectProductsByParentCategoryIDIfRoleAdmin @FK_iParentCategoryID", parentCategoryParam);
    }

    public IEnumerable<Discount> getDiscounts()
    {
        return _context.Discounts.FromSqlRaw("sp_GetDiscounts");
    }

    public IEnumerable<TransportPrice> getTransportPrice()
    {
        return _context.TransportPrices.FromSqlRaw("sp_GetTransportPrice");
    }

    public bool updateProduct(int productID, int storeID, int categoryID, int discountID, int transportID, string productName, int quantity, string productDescription, string imageUrl, double price)
    {
        SqlParameter productIDParam = new SqlParameter("@PK_iProductID", productID);
        SqlParameter storeIDParam = new SqlParameter("@FK_iStoreID", storeID);
        SqlParameter categoryIDParam = new SqlParameter("@FK_iCategoryID", categoryID);
        SqlParameter discountIDParam = new SqlParameter("@FK_iDiscountID", discountID);
        SqlParameter transportIDParam = new SqlParameter("@FK_iTransportID", transportID);
        SqlParameter productNameParam = new SqlParameter("@sProductName", productName);
        SqlParameter quantityParam = new SqlParameter("@iQuantity", quantity);
        SqlParameter productDescParam = new SqlParameter("@sProductDescription", productDescription);
        SqlParameter imageUrlParam = new SqlParameter("@sImageUrl", imageUrl);
        SqlParameter priceParam = new SqlParameter("@dPrice", price);
        SqlParameter isVisibleParam = new SqlParameter("@iIsVisible", 1);
        SqlParameter updateTimeParam = new SqlParameter("@dUpdateTime", DateTime.Now);
        _context.Database.ExecuteSqlRaw("sp_UpdateProduct @PK_iProductID, @FK_iStoreID, @FK_iCategoryID, @FK_iDiscountID, @FK_iTransportID, @sProductName, @iQuantity, @sProductDescription, @sImageUrl, @dPrice, @iIsVisible, @dUpdateTime",
            productIDParam,
            storeIDParam,
            categoryIDParam,
            discountIDParam,
            transportIDParam,
            productNameParam,
            quantityParam,
            productDescParam,
            imageUrlParam,
            priceParam,
            isVisibleParam,
            updateTimeParam
        );
        return true;
    }

    public bool insertProduct(int storeID, int categoryID, int discountID, int transportID, string productName, int quantity, string productDescription, string imageUrl, double price)
    {
        SqlParameter storeIDParam = new SqlParameter("@FK_iStoreID", storeID);
        SqlParameter categoryIDParam = new SqlParameter("@FK_iCategoryID", categoryID);
        SqlParameter discountIDParam = new SqlParameter("@FK_iDiscountID", discountID);
        SqlParameter transportIDParam = new SqlParameter("@FK_iTransportID", discountID);
        SqlParameter productNameParam = new SqlParameter("@sProductName", productName);
        SqlParameter quantityParam = new SqlParameter("@iQuantity", quantity);
        SqlParameter productDescParam = new SqlParameter("@sProductDescription", productDescription);
        SqlParameter imageUrlParam = new SqlParameter("@sImageUrl", imageUrl);
        SqlParameter priceParam = new SqlParameter("@dPrice", price);
        SqlParameter isVisibleParam = new SqlParameter("@iIsVisible", 1);
        SqlParameter createTimeParam = new SqlParameter("@dCreateTime", DateTime.Now);
        SqlParameter updateTimeParam = new SqlParameter("@dUpdateTime", DateTime.Now);
        _context.Database.ExecuteSqlRaw("EXEC sp_InsertProduct @FK_iStoreID, @FK_iCategoryID, @FK_iDiscountID, @FK_iTransportID, @sProductName, @iQuantity, @sProductDescription, @sImageUrl, @dPrice, @iIsVisible, @dCreateTime, @dUpdateTime", 
            storeIDParam,
            categoryIDParam,
            discountIDParam,
            transportIDParam,
            productNameParam,
            quantityParam,
            productDescParam,
            imageUrlParam,
            priceParam,
            isVisibleParam,
            createTimeParam,
            updateTimeParam
        );
        return true;
    }

    public IEnumerable<Product> searchProductByKeyword(string keyword)
    {
        SqlParameter keywordParam = new SqlParameter("@sKeyword", keyword);
        return _context.Products.FromSqlRaw("EXEC sp_SearchProductsByKeyword @sKeyword", keywordParam);
    }

    public IEnumerable<Product> checkProductInCart(int productID)
    {
        SqlParameter productIDParam = new SqlParameter("@PK_iProductID", productID);
        return _context.Products.FromSqlRaw("EXEC sp_CheckProductInCart @PK_iProductID", productIDParam);
    }

    public IEnumerable<Product> checkProductInOrder(int productID)
    {
        SqlParameter productIDParam = new SqlParameter("@PK_iProductID", productID);
        return _context.Products.FromSqlRaw("EXEC sp_CheckProductInOrder @PK_iProductID", productIDParam);
    }

    public bool deleteProductByID(int productID)
    {
        SqlParameter productIDParam = new SqlParameter("@PK_iProductID", productID);
        _context.Database.ExecuteSqlRaw("EXEC sp_DeleteProductByID @PK_iProductID", productIDParam);
        return true;
    }

    public bool insertProductReviewer(int userID, int productID, int star, string comment, string image)
    {
        SqlParameter userIDParam = new SqlParameter("@FK_iUserID", userID);
        SqlParameter productIDParam = new SqlParameter("@FK_iProductID", productID);
        SqlParameter starParam = new SqlParameter("@iStar", star);
        SqlParameter commentParam = new SqlParameter("@sComment", comment);
        SqlParameter imageParam = new SqlParameter("@sReviewerImage", image);
        SqlParameter createTimeParam = new SqlParameter("@dCreateTime", DateTime.Now);
        SqlParameter updateTimeParam = new SqlParameter("@dUpdateTime", DateTime.Now);
        _context.Database.ExecuteSqlRaw("EXEC sp_InsertProductReviewer @FK_iUserID, @FK_iProductID, @iStar, @sComment, @sReviewerImage, @dCreateTime, @dUpdateTime", 
            userIDParam,
            productIDParam,
            starParam,
            commentParam,
            imageParam,
            createTimeParam,
            updateTimeParam
        );
        return true;
    }

    public IEnumerable<Reviewer> getReviewerByProductID(int productID)
    {
        SqlParameter productIDParam = new SqlParameter("@FK_iProductID", productID);
        return _context.Reviewers.FromSqlRaw("EXEC sp_GetReviewerByProductID @FK_iProductID", productIDParam);
    }

    public IEnumerable<Favorite> getFavoritesByProductID(int productID)
    {
        SqlParameter productIDParam = new SqlParameter("@FK_iProductID", productID);
        return _context.Favorites.FromSqlRaw("EXEC sp_GetFavoritesByProductID @FK_iProductID", productIDParam);
    }

    public IEnumerable<Favorite> getFavoritesByProductIDAndUserID(int productID, int userID)
    {
        SqlParameter productIDParam = new SqlParameter("@FK_iProductID", productID);
        SqlParameter userIDParam = new SqlParameter("@FK_iUserID", userID);
        return _context.Favorites.FromSqlRaw("EXEC sp_GetFavoritesByProductIDAndUserID @FK_iProductID, @FK_iUserID", productIDParam, userIDParam);
    }

    public bool insertFavorite(int userID, int productID)
    {
        SqlParameter userIDParam = new SqlParameter("@FK_iUserID", userID);
        SqlParameter productIDParam = new SqlParameter("@FK_iProductID", productID);
        _context.Database.ExecuteSqlRaw("EXEC sp_InsertFavorite @FK_iUserID, @FK_iProductID", userIDParam, productIDParam);
        return true;
    }

    public bool deleteFavorite(int userID, int productID)
    {
        SqlParameter userIDParam = new SqlParameter("@FK_iUserID", userID);
        SqlParameter productIDParam = new SqlParameter("@FK_iProductID", productID);
        _context.Database.ExecuteSqlRaw("EXEC sp_DeleteFavorite @FK_iUserID, @FK_iProductID", userIDParam, productIDParam);
        return true;
    }

    public IEnumerable<Reviewer> getReviewerByID(int reviewerID)
    {
        SqlParameter reviewerIDParam = new SqlParameter("@PK_iReviewerID", reviewerID);
        return _context.Reviewers.FromSqlRaw("EXEC sp_GetReviewerByID @PK_iReviewerID", reviewerIDParam);
    }

    public bool updateReviewer(int reviewerID, int userID, int productID, int star, string comment, string image)
    {
        SqlParameter reviewerIDParam = new SqlParameter("@PK_iReviewerID", reviewerID);
        SqlParameter userIDParam = new SqlParameter("@FK_iUserID", userID);
        SqlParameter productIDParam = new SqlParameter("@FK_iProductID", productID);
        SqlParameter starParam = new SqlParameter("@iStar", star);
        SqlParameter commentParam = new SqlParameter("@sComment", comment);
        SqlParameter updateTimeParam = new SqlParameter("@dUpdateTime", DateTime.Now);
        SqlParameter imageParam = new SqlParameter("@sReviewerImage", image);
        _context.Database.ExecuteSqlRaw("EXEC sp_UpdateReviewer @PK_iReviewerID, @FK_iUserID, @FK_iProductID, @iStar, @sComment, @dUpdateTime, @sReviewerImage", 
            reviewerIDParam,
            userIDParam,
            productIDParam,
            starParam,
            commentParam,
            updateTimeParam,
            imageParam
        );
        return true;
    }

    public bool deleteReviewer(int reviewerID)
    {
        SqlParameter reviewerIDParam = new SqlParameter("@PK_iReviewerID", reviewerID);
        _context.Database.ExecuteSqlRaw("EXEC sp_DeleteReviewer @PK_iReviewerID", reviewerIDParam);
        return true;
    }

    public IEnumerable<Product> getProductsByIndustryIDAndSortIncre(int industryID)
    {
        SqlParameter industryIDParam = new SqlParameter("@FK_iIndustryID", industryID);
        return _context.Products.FromSqlRaw("EXEC sp_GetProductsByIndustryIDAndSortIncre @FK_iIndustryID", industryIDParam);
    }

    public IEnumerable<Product> getProductsByIndutryIDAndSortReduce(int industryID)
    {
        SqlParameter industryIDParam = new SqlParameter("@FK_iIndustryID", industryID);
        return _context.Products.FromSqlRaw("EXEC sp_GetProductsByIndutryIDAndSortReduce @FK_iIndustryID", industryIDParam);
    }

    public IEnumerable<Product> getProductsByShopIDAndSortIncre(int shopID)
    {
        SqlParameter shopIDParam = new SqlParameter("@FK_iShopID", shopID);
        return _context.Products.FromSqlRaw("EXEC sp_GetProductsByShopIDAndSortIncre @FK_iShopID", shopIDParam);
    }

    public IEnumerable<Product> getProductsByShopIDAndSortReduce(int shopID)
    {
        SqlParameter shopIDParam = new SqlParameter("@FK_iShopID", shopID);
        return _context.Products.FromSqlRaw("EXEC sp_GetProductsByShopIDAndSortReduce @FK_iShopID", shopIDParam);
    }
}