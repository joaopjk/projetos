package campaign

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

var (
	name    = "Campaign X"
	content = "Hello World"
	contact = []string{"email1@com", "email2@com"}
)

func TestNewCampaign(t *testing.T) {
	assertions := assert.New(t)

	campaign, _ := NewCampaign(name, content, contact)

	assertions.Equal(name, campaign.Name)
	assertions.Equal(content, campaign.Content)
	assertions.Equal(len(contact), len(campaign.Contacts))
}

func TestNewCampaign_IdIsNotNil(t *testing.T) {
	assertions := assert.New(t)

	campaign, _ := NewCampaign(name, content, contact)

	assertions.NotNil(campaign.Id)
}

func TestNewCampaign_CreatedOnsNotNil(t *testing.T) {
	assertions := assert.New(t)

	campaign, _ := NewCampaign(name, content, contact)

	assertions.NotNil(campaign.CreatedOn)
}

func TestNewCampaign_MustValidateName(t *testing.T) {
	assertions := assert.New(t)

	_, err := NewCampaign("", content, contact)

	assertions.Equal("name is required", err.Error())
}

func TestNewCampaign_MustValidateContent(t *testing.T) {
	assertions := assert.New(t)

	_, err := NewCampaign(name, "", contact)

	assertions.Equal("content is required", err.Error())
}

func TestNewCampaign_MustValidateContacts(t *testing.T) {
	assertions := assert.New(t)

	_, err := NewCampaign(name, content, []string{})

	assertions.Equal("emails is required", err.Error())
}
