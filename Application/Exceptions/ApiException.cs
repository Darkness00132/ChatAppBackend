namespace Application.Exceptions
{
    public sealed class ApiException : Exception
    {
        public int StatusCode { get; }

        public ApiException(int statusCode, string message) : base(message)
            => StatusCode = statusCode;

        public static ApiException NotFound(string msg = "العنصر المطلوب غير موجود.") => new(404, msg);
        public static ApiException BadRequest(string msg = "البيانات المُرسلة غير صحيحة. برجاء المراجعة والمحاولة مرة أخرى.") => new(400, msg);
        public static ApiException Unauthorized(string msg = "غير مصرح. برجاء تسجيل الدخول ثم المحاولة مرة أخرى.") => new(401, msg);
        public static ApiException Forbidden(string msg = "ليس لديك صلاحية لإتمام هذا الإجراء.") => new(403, msg);
        public static ApiException Conflict(string msg = "حدث تعارض في البيانات. قد يكون الطلب مكررًا أو البيانات موجودة بالفعل.") => new(409, msg);
        public static ApiException InternalServerError(string msg = "حدث خطأ داخلي في الخادم، يرجى المحاولة لاحقاً.") => new(500, msg);
    }
}
