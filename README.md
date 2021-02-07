

## Slack
1. Create a Slack Application on the [Slack API website](https://api.slack.com/apps).
2. When created, find "OAuth & Permissions" under "Features". On the page, scroll down until you see "Scopes" and "Bot Token Scopes"
3. Click the "Add an OAuth Scope" and add:
    - `chat:write` - Needed to post messages as the bot
    - `channels:read` ?
    - `im:write`?
    - `groups:read` - Needed if the bot will send messages to private channels
    - `users:read.email`- Needed to look up a user by email. This is done for retrieving their Slack profile picture.
4. At the top of "OAuth & Permissions", there should be a "Add to workspace". Walk through the process of adding it to your workspace
5. An OAuth token starting with `xoxb-` should now be visible at the top. This token will be referred to as the **bot oauth token**.

Needed configuration for Slack:  
`Slack__Channel`: Which channel to use for updates on PullRequests (and builds). Find your channel ID by rightclicking a channel, copying the link and using the ID at the end of the url.  
`Slack__OAuthToken`: The Bot OAuth token that will be used to interact with your Slack workspace. This should start with `xoxb-`. If you don't know how to get a bot token, follow the steps above.  

## Azure DevOps
1. Create a Personal Access Token at `https://dev.azure.com/<<Your Organization>>/_usersSettings/tokens`
2. The scopes needed for CollabAssist to work are `Build:Read` and `Code:Read&Write`.

Needed configuration for Azure DevOps:   
`DevOps__BaseUrl`: Full URL of Azure DevOps containing your organization name. (e.g. `https://dev.azure.com/collabassist/`)  
`DevOps__PersonalAccessToken`: The PAT used to interact with your Azure DevOps environment. If you don't know how to get a personal access token, follow the steps above.  