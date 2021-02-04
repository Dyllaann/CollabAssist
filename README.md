

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