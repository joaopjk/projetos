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

	campaign := NewCampaign(name, content, contact)

	assertions.Equal(name, campaign.Name)
	assertions.Equal(content, campaign.Content)
	assertions.Equal(len(contact), len(campaign.Contacts))
}

func TestNewCampaign_IdIsNotNil(t *testing.T) {
	assertions := assert.New(t)

	campaign := NewCampaign(name, content, contact)

	assertions.NotNil(campaign.Id)
}

func TestNewCampaign_CreatedOnsNotNil(t *testing.T) {
	assertions := assert.New(t)

	campaign := NewCampaign(name, content, contact)

	assertions.NotNil(campaign.CreatedOn)
}
