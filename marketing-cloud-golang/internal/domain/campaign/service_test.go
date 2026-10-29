package campaign

import (
	"emailn/internal/contract"
	"errors"
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

	repository = new(repositoryMock)
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

	repository.On("Save", mock.Anything).Return(nil)
	service := Service{Repository: repository}

	_, err := service.CreateCampaign(newCampaign)

	assertions.Nil(err)
}

func TestService_NewCampaign(t *testing.T) {
	repository.On("Save", mock.MatchedBy(func(campaign Campaign) bool {
		if campaign.Name != newCampaign.Name ||
			campaign.Content != newCampaign.Content ||
			len(campaign.Content) != len(newCampaign.Content) {
			return false
		}
		return true
	})).Return(nil)
	service := Service{Repository: repository}

	service.CreateCampaign(newCampaign)

	repository.AssertExpectations(t)
}

func TestService_NewCampaign_Error(t *testing.T) {
	assertions := assert.New(t)
	newCampaign.Name = ""

	service := Service{Repository: repository}
	_, err := service.CreateCampaign(newCampaign)

	assertions.NotNil(err)
}

func Test_Create_ValidateRepositorySave(t *testing.T) {
	assertions := assert.New(t)

	repository.On("Save", mock.Anything).Return(errors.New("repository save error"))

	service := Service{Repository: repository}
	_, err := service.CreateCampaign(newCampaign)

	assertions.NotNil(err)
}
