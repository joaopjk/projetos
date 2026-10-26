package campaign

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestNewCampaign(t *testing.T) {
	assertions := assert.New(t)

	name := "Campaign X"
	content := "Hello World"
	contact := []string{"email1@com", "email2@com"}

	campaign := NewCampaign(name, content, contact)

	assertions.Equal(name, campaign.Name)
	assertions.Equal(content, campaign.Content)
	assertions.Equal(len(contact), len(campaign.Contacts))
}
