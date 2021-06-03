using System;
using CollabAssist.Incoming.DevOps.Models;

namespace CollabAssist.Test.Unit
{
    public static class TestUtils
    {
        public static DevOpsPullRequestNotification GenerateValidDevOpsPrNotification()
        {
            return new DevOpsPullRequestNotification()
            {
                SubscriptionId = new Guid("e5fa19dd-faf4-4c44-a729-d051a0ee9420"),
                NotificationId = 1234,
                Id = new Guid("1b9abdcc-df22-48d2-9f38-ee87723418a6"),
                Resource = new PullRequestResource
                {
                    Repository = new Repository()
                    {
                        Id = new Guid("ae026029-06fe-4e60-8f40-7006f91ab67d"),
                        Name = "CollabAssist",
                        Url = "https://dev.azure.com/collabassistorg/5394b153-e9fc-47a8-88c6-4f1cc6f9184c/_apis/git/repositories/ae026029-06fe-4e60-8f40-7006f91ab67d",
                        Project = new Project()
                        {
                            Id = new Guid("5394b153-e9fc-47a8-88c6-4f1cc6f9184c"),
                            Name = "CollabAssistProject",
                            Description = "Assistant for all your Collaboration needs",
                            Url = "https://dev.azure.com/collabassistorg/_apis/projects/5394b153-e9fc-47a8-88c6-4f1cc6f9184c",
                            State = "wellFormed",
                            Revision = 1,
                            Visibility = "private",
                            LastUpdateTime = new DateTime(2020, 10, 24, 20, 00, 00)
                        }
                    },
                    PullRequestId = 1337,
                    CodeReviewId = 4321,
                    Status = "active",
                    CreatedBy = new CreatedBy
                    {
                        DisplayName = "Dyllaann",
                        UniqueName = "contact@dylan.dev",
                        ImageUrl = "https://avatars1.githubusercontent.com/u/7451618?s=460&u=4d63142c0e1c3848c117ba803f325298ad857923&v=4"
                    },
                    Title = "This is a valid PR",
                    Description = "This is just a valid PR model",
                    MergeStatus = "active",
                    IsDraft = false,
                },
                CreatedDate = new DateTime(2020, 10, 25, 00, 15, 00),
            };
        }

        public static DevOpsPullRequestNotification GenerateValidDevOpsPrNotificationWithSpacesInNames()
        {
            return new DevOpsPullRequestNotification()
            {
                SubscriptionId = new Guid("e5fa19dd-faf4-4c44-a729-d051a0ee9420"),
                NotificationId = 1234,
                Id = new Guid("1b9abdcc-df22-48d2-9f38-ee87723418a6"),
                Resource = new PullRequestResource
                {
                    Repository = new Repository()
                    {
                        Id = new Guid("ae026029-06fe-4e60-8f40-7006f91ab67d"),
                        Name = "Collab Assist",
                        Url = "https://dev.azure.com/collabassistorg/5394b153-e9fc-47a8-88c6-4f1cc6f9184c/_apis/git/repositories/ae026029-06fe-4e60-8f40-7006f91ab67d",
                        Project = new Project()
                        {
                            Id = new Guid("5394b153-e9fc-47a8-88c6-4f1cc6f9184c"),
                            Name = "CollabAssist Project",
                            Description = "Assistant for all your Collaboration needs",
                            Url = "https://dev.azure.com/collabassistorg/_apis/projects/5394b153-e9fc-47a8-88c6-4f1cc6f9184c",
                            State = "wellFormed",
                            Revision = 1,
                            Visibility = "private",
                            LastUpdateTime = new DateTime(2020, 10, 24, 20, 00, 00)
                        }
                    },
                    PullRequestId = 1337,
                    CodeReviewId = 4321,
                    Status = "active",
                    CreatedBy = new CreatedBy
                    {
                        DisplayName = "Dyllaann",
                        UniqueName = "contact@dylan.dev",
                        ImageUrl = "https://avatars1.githubusercontent.com/u/7451618?s=460&u=4d63142c0e1c3848c117ba803f325298ad857923&v=4"
                    },
                    Title = "This is a valid PR",
                    Description = "This is just a valid PR model",
                    MergeStatus = "active",
                    IsDraft = false,
                },
                CreatedDate = new DateTime(2020, 10, 25, 00, 15, 00),
            };
        }
    }
}