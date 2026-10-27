package campaign

import "emailn/internal/contract"

type Service struct {
	Repository Repository
}

func (s *Service) CreateCampaign(newCampaign contract.NewCampaign) (string, error) {

	campaign := Campaign{Name: newCampaign.Name, Content: newCampaign.Content}
	s.Repository.Save(campaign)

	return campaign.Id, nil
}
