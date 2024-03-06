using System.ComponentModel.DataAnnotations;

namespace TestAPP.Data
{
    public class 사용자
    {
        public int Id { get; set; }
        [Required]
        public string 아이디 { get; set; }
        public string 비밀번호 { get; set; }
        public string 이메일 { get; set; }
        public string 이름 { get; set; }
        public string 소속 { get; set; }
        public string 생년월일 { get; set; }
        public string 전화번호 { get; set; }

        public List<계정권한> 계정권한들 { get; set; }
    }

    public class 계정권한
    {
        public int Id { get; set; }
        [Required]
        public string 권한분류 { get; set; }
    }
}
