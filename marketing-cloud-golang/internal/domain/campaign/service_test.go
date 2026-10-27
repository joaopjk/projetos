package campaign

import (
	"emailn/internal/contract"
	"testing"

	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/mock"
)

var (
	newCampaign = contract.NewCampaign{
		Name:    "name",
		Content: "content",
		Emails:  []string{"email1", "email2"},
	}
)

type repositoryMock struct {
	mock.Mock
}

func (r *repositoryMock) Save(campaign Campaign) error {
	args := r.Called(campaign)
	return args.Error(0)
}

func TestService_CreateCampaign(t *testing.T) {
	assertions := assert.New(t)
	repository := new(repositoryMock)
	repository.On("Save", mock.Anything).Return(nil)
	service := Service{Repository: repository}

	_, err := service.CreateCampaign(newCampaign)

	assertions.Nil(err)
}

func TestService_NewCampaign(t *testing.T) {
	repository := new(repositoryMock)
	repository.On("Save", mock.MatchedBy(func(campaign Campaign) bool {
		if campaign.Name != newCampaign.Name {
			return false
		}
		if campaign.Content != newCampaign.Content {
			return false
		}
		if len(campaign.Content) != len(newCampaign.Content) {
			return false
		}
		return true
	})).Return(nil)
	service := Service{Repository: repository}

	service.CreateCampaign(newCampaign)

	repository.AssertExpectations(t)
}
