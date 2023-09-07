protected async Task<ActionResult> GetAllAsync()
{
    string prefUser = User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
    var requestorGroup = await MicrosoftGroupsAuthentication.GetRequestorGroup(prefUser);
    var roles = MicrosoftGroupsAuthentication.ReturnRole(requestorGroup);

    if (!roles.Any())
    {
        _logger.LogError(prefUser + " is not a member of an authorised group");
        return Unauthorized(new Response { Status = false });
    }

    try
    {
        [...]
        Controller code here
        [...]

        return Ok(new Response { Status = true, Object = finalData });
    }
    catch (Exception exp)
    {
        _logger.LogError(exp.ToString());
        return BadRequest(new Response { Status = false });
    }
}