# MailService_ASPCore
Gửi Mail bằng cách sử dụng Gmail :
  -Tạo SmtpClient kết nối đến smtp.gmail.com
  -Xây dựng phương thực gửi mail  trong file MailUtils/MailUtils.cs
  -Thiết lập bảo mật tài khoản Email && Tài khoản gmail cần bật IMAP.
  


Dùng MailKit gửi Mail trong ASP.NET với Gmail :
Phải cài đặt 2 package sau : 
    dotnet add package MailKit
    dotnet add package MimeKit
 Sau đó thực hiện các bước sau : 
 -Xây dựng nội dung session MailSettings trong fil appsettings.json
 -Xây dựng lớp Service/MailSettings.cs có các thuộc tính cùng kiểu dữ liệu trong với MailSettinsg ở file appsettings.json
 
 -Đăng ký dịch vụ MailSettings vào trong file Startup.cs
    + Quá tải phương thức khởi tạo ở file Startup, nhận param là IConfiguration
    +Tạo một thuộc tính có kiểu IConfiguration
    
 -Xây dựng lớp Service/SendMailService.cs, có sử dụng kỹ thuật DI MailSettings/MailContent.
    
    
    
    
