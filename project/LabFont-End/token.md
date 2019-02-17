```c#
if (ModelState.IsValid)
{
    if (!_analysis.GetLoginUserConfig(HttpContext).Power.QuestionBankManager)
    {
        return Json(new
        {
            isOk = false,
            title = "错误提示",
            message = "你并无题库管理操作权限"
        });
    }
}
else
{
    List<string> sb = new List<string>();
    List<string> Keys = ModelState.Keys.ToList();
    foreach (var key in Keys)
    {
        var errors = ModelState[key].Errors.ToList();
        //将错误描述添加到sb中
        foreach (var error in errors)
        {
            sb.Add(error.ErrorMessage);
        }
    }
    return Json(new
    {
        isOk = false,
        error = sb,
        title = "错误提示",
        message = "参数错误,传递了不符合规定的参数"
    });
}
```