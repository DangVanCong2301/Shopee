# Sàn giao dịch thương mại (Shopee) - Đồ án tốt nghiệp
# Công nghệ: ASP.NET Core MVC  7.0
- Luồng sử lý dữ liệu cơ bản: User->Routeing->Controller<->Model->View->User

## Thành phần chính
### Routing
- Dựa vào các request Để gọi các Controller

### Controller
- Thực hiện các logic code từ yêu cầu cảu request và trả về response

### View
- Giao diện HTML, CSS được trả về theo logic của controller

### Model
- Được sử dụng để tương tác với các trường dữ liệu của bảng (định nghĩa các field, primary keys, foreign key,...)
- Khi tương tác với các dữ liệu có thể viết vào 1 file Repository riêng

## Khác
### Repository 
- Nơi tương tác với các dữ liệu của thực thể
- Được sử dụng để lấy dữ liệu và tương tác với dữ liệu của table (create, read, update, delete)

### Viết thủ tục lưu trong CSDL
  - Thủ tục được viết trên Server và khi truy vấn ta chỉ cần gọi thủ tục đó
### Quy tắc đặt tên trong CSDL
 - Tên Database: db_ (Ví dụ: db_F4_Shop)
 - Tên bảng: tbl_ (Ví dụ tbl_Categories)
 - Tên thủ tục: sp_ (Ví dụL sp_GetCategories)
 - ...
## Công việc của từng Dev
Chi tiết tại: https://docs.google.com/document/d/1OA526wTnw-2Jn4faBllxIqHbD78u58EU/edit?usp=drive_link&ouid=102969611045986692309&rtpof=true&sd=true
## Kết quả thực hiện
### Trang chủ
![image](https://github.com/DangVanCong2301/Shopee/assets/111124018/f76fecd0-c289-464e-a798-cb0b5d3d7796)
### Trang sản phẩm
![image](https://github.com/DangVanCong2301/F4-Shop/assets/111124018/9126d19c-cc46-49c9-87cf-ac0b98ab04c6)
### Chi tiết sản phẩm
![image](https://github.com/DangVanCong2301/Shopee/assets/111124018/ee883b67-b0f9-4eef-a330-3cfb9afd57df)
### Bình luận, đánh giá sản phẩm
![image](https://github.com/DangVanCong2301/F4-Shop/assets/111124018/92aafd63-a34e-4092-a23b-f2e344673e9f)
### Giỏ hàng
![image](https://github.com/DangVanCong2301/F4-Shop/assets/111124018/a8e018a9-86e4-4cbd-aca2-29a3d112b676)
### Quản lý thông tin tài khoản
![image](https://github.com/DangVanCong2301/F4-Shop/assets/111124018/bef0bda4-ea4b-46eb-af38-e77e64aee75f)
### Đặt hàng
![image](https://github.com/DangVanCong2301/F4-Shop/assets/111124018/30261bcb-5b7f-48aa-aa36-315cda07caae)










