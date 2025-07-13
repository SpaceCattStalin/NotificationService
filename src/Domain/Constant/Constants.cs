namespace Domain.Constant
{
    public static class Constants
    {
        public class ResponseCodeConstants
        {
            public const string NOT_FOUND = "Not found!";
            public const string SUCCESS = "Success!";
            public const string FAILED = "Failed!";
            public const string EXISTED = "Existed!";
            public const string DUPLICATE = "Duplicate!";
            public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";
            public const string INVALID_INPUT = "Invalid input!";
            public const string UNAUTHORIZED = "Unauthorized!";
            public const string FORBIDDEN = "Forbidden!";
            public const string BADREQUEST = "Bad request!";
        }

        public class MessagesConstants
        {
            public const string DELETE_SUCCESS = "Xóa thành công";
            public const string UPDATE_SUCCESS = "Cập nhật thành công";
            public const string CREATE_SUCCESS = "Tạo mới thành công";
            public const string SUCCESS = "Thành công";

        }

        public static class ValidationMessages
        {
            public const string InvalidCode = "Mã sản phẩm không được dưới 3 kí tự, không vượt quá 50 kí tự và không chứa kí tự đặc biệt.";
            public const string InvalidName = "Tên sản phẩm không được để trống và không quá 50 ký tự.";
            public const string InvalidBackGroundColor = "Mã màu nền không hợp lệ. Vui lòng đảm bảo nó không quá 20 ký tự.";
            public const string InvalidTextColor = "Mã màu chữ không hợp lệ. Vui lòng đảm bảo nó không quá 20 ký tự.";
            public const string InvalidImgFileFormat = "Hình ảnh không hợp lệ. Vui lòng cung cấp định dạng hợp lệ.";
            public const string InvalidImgFileSize = "Hình ảnh có kích cỡ hơn 5MB. Vui lòng cung cấp hình ảnh có kích cỡ khác";
            public const string InvalidCategoryId = "Mã danh mục sản phẩm không được để trống.";

            public const string InvalidData = "Có lỗi khi valid data";
            public const string NotFoundId = "không tìm thấy Id";
            public const string ExistId = "Mã Id danh mục đã tồn tại.";
            public const string ExistCode = "Mã code danh mục đã tồn tại.";
            public const string IsSoftDelete = "Item đã được xóa mềm trước đó.";
            public const string NotYetSoftDelete = "Item chưa được xóa mềm.";
            public const string itemInUse = "Item đang được sử dụng.";
        }

        //public static class ApiEndpoints
        //{
        //    public const string CategoryApi = "/api/category/";
        //}
    }
}
