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
    return Json(new
    {
        isOk = false,
        error = _analysis.ModelStateDictionaryError(ModelState),
        title = "错误提示",
        message = "参数错误,传递了不符合规定的参数"
    });
}
```