using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommonClass
/// </summary>
public class CommonClass
{
    public CommonClass()
    {
    }

}
public class Tokenclass
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
    public string refresh_token { get; set; }
}
public class Userclass
{
    public string id { get; set; }
    public string name { get; set; }
    public string given_name { get; set; }
    public string family_name { get; set; }
    public string link { get; set; }
    public string picture { get; set; }
    public string gender { get; set; }
    public string locale { get; set; }
    public string email { get; set; }
}

/// <summary>
/// API
/// </summary>
public class Response
{
    public Response()
    {
        StatusCode = "200";
    }
    public string success { get; set; }
    public string message { get; set; }
    public string StatusCode { get; set; }
    public Users Users { get; set; }
    public StudentResult StudentResult { get; set; }
    public Blank Blank { get; set; }
}
public class Blank
{


}

#region User Login
public class Users
{
    public string Token { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

#endregion

#region StudentResult
public class StudentResult
{
    public Students[] Students { get; set; }
}
public class Students
{
    public string CasesID { get; set; }
    public string SurName { get; set; }
    public string FirstName { get; set; }
    public string PrefName { get; set; }
    public string HomeGroup { get; set; }
    public string Status { get; set; }
    public Category[] Category { get; set; }
    //public Categories[] Categories { get; set; }

}
public class Category
{
    public string Name { get; set; }
    public SubCategory[] SubCategory { get; set; }
}
public class SubCategory
{
    public string Name { get; set; }
    public Judgement Judgement { get; set; }
}
public class Judgement
{
    public string Formatted { get; set; }
    public decimal Raw { get; set; }
}

#endregion

#region Student Result with Sem1 & Sem2
public class Responses
{
    public Responses()
    {
        StatusCode = "200";
    }
    public string success { get; set; }
    public string message { get; set; }
    public string StatusCode { get; set; }
    public StudentResults StudentResult { get; set; }
}
public class StudentResults
{
    public StudentList[] Students { get; set; }
}
public class StudentList
{
    public string CasesID { get; set; }
    public string SurName { get; set; }
    public string FirstName { get; set; }
    public string PrefName { get; set; }
    public string HomeGroup { get; set; }
    public string Status { get; set; }

    public Categories[] Category { get; set; }

}
public class Categories
{
    public string Name { get; set; }
    public SubCategories[] SubCategory { get; set; }
}
public class SubCategories
{
    public string Name { get; set; }
    public Judgements Judgements { get; set; }
}
public class Judgements
{
    public Levels[] Levels { get; set; }

}
public class Levels
{
    public int Year { get; set; }
    public decimal? S1Raw { get; set; }
    public string S1Formatted { get; set; }
    public decimal? S2Raw { get; set; }
    public string S2Formatted { get; set; }
    public decimal? Change { get; set; }
}
#endregion

#region Student Detailed Result 
public class ResponsesDetail
{
    public ResponsesDetail()
    {
        StatusCode = "200";
    }
    public string success { get; set; }
    public string message { get; set; }
    public string StatusCode { get; set; }
    public StudentResultDetail StudentResult { get; set; }
}
public class StudentResultDetail
{
    public StudentListDetail[] Students { get; set; }
}
public class StudentListDetail
{
    public string CasesID { get; set; }
    public string SurName { get; set; }
    public string FirstName { get; set; }
    public string PrefName { get; set; }
    public string HomeGroup { get; set; }
    public string Status { get; set; }

    public CategoriesDetail[] Category { get; set; }

}
public class CategoriesDetail
{
    public string Name { get; set; }
    public SubCategoriesDetail[] SubCategory { get; set; }
}
public class SubCategoriesDetail
{
    public string Name { get; set; }
    public JudgementsDetail Judgements { get; set; }
}
public class JudgementsDetail
{
    public decimal? Raw { get; set; }
    public string Formatted { get; set; }
    public LevelsDetail[] Levels { get; set; }

}
public class LevelsDetail
{
    public string Name { get; set; }
    public Questions[] Questions { get; set; }
}

public class Questions
{
    public string Title { get; set; }
    public Demonstration1 Demonstration1 { get; set; }
    public Demonstration2 Demonstration2 { get; set; }
    public Demonstration3 Demonstration3 { get; set; }
}

public class Demonstration1
{
    public string Date { get; set; }
    public string SourceOfEvidence { get; set; }
    public Attachment[] Attachment { get; set; }

}

public class Demonstration2
{
    public string Date { get; set; }
    public string SourceOfEvidence { get; set; }
    public Attachment[] Attachment { get; set; }

}

public class Demonstration3
{
    public string Date { get; set; }
    public string SourceOfEvidence { get; set; }
    public Attachment[] Attachment { get; set; }

}
public class Attachment
{
    public string URL { get; set; }
}


#endregion

#region Student Detailed Result With Skill
public class ResponsesDetailSkill
{
    public ResponsesDetailSkill()
    {
        StatusCode = "200";
    }
    public string success { get; set; }
    public string message { get; set; }
    public string StatusCode { get; set; }
    public StudentResultDetailSkill StudentResult { get; set; }
}
public class StudentResultDetailSkill
{
    public StudentListDetailSkill[] Students { get; set; }
}
public class StudentListDetailSkill
{
    public string CasesID { get; set; }
    public string SurName { get; set; }
    public string FirstName { get; set; }
    public string PrefName { get; set; }
    public string HomeGroup { get; set; }
    public string Status { get; set; }

    public CategoriesDetailSkill[] Category { get; set; }

}
public class CategoriesDetailSkill
{
    public string Name { get; set; }
    public SubCategoriesDetailSkill[] SubCategory { get; set; }
}
public class SubCategoriesDetailSkill
{
    public string Name { get; set; }
    public JudgementsDetailSkill Judgements { get; set; }
}
public class JudgementsDetailSkill
{
    public decimal? Raw { get; set; }
    public string Formatted { get; set; }
    public LevelsDetailSkill[] Levels { get; set; }

}
public class LevelsDetailSkill
{
    public string Name { get; set; }
    public QuestionsSkill[] Questions { get; set; }
}

public class QuestionsSkill
{
    public string Title { get; set; }
    public QuestionsSubheadings[] Subheadings { get; set; }
    public Demonstration1 Demonstration1 { get; set; }
    public Demonstration2 Demonstration2 { get; set; }
    public Demonstration3 Demonstration3 { get; set; }
}

public class QuestionsSubheadings
{
    public string Title { get; set; }
    public Skill[] Skills { get; set; }
   
}

public class Skill
{
    public string Title { get; set; }
    public Demonstration  Demonstration  { get; set; }
   
}

public class Demonstration 
{
    public string Date { get; set; }
    public string SourceOfEvidence { get; set; }
    public AttachmentSkill[] Attachment { get; set; }

}
public class AttachmentSkill
{
    public string URL { get; set; }
}


#endregion