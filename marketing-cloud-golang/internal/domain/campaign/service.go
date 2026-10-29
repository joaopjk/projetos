package campaign

import "emailn/internal/contract"

type Service struct {
	Repository Repository
}

func (s *Service) CreateCampaign(newCampaign contract.NewCampaign) (string, error) {

	campaign, err := NewCampaign(newCampaign.Name, newCampaign.Content, newCampaign.Emails)
	if err != nil {
		return "", err
	}

	err = s.Repository.Save(*campaign)
	if err != nil {
		return "", err
	}

	return campaign.Id, nil
}
