package internalErrors

import (
	"errors"

	"github.com/go-playground/validator/v10"
)

func ValidateStruct(s interface{}) error {
	validate := validator.New()

	err := validate.Struct(s)
	if err == nil {
		return nil
	}

	var validationErrors validator.ValidationErrors
	errors.As(err, &validationErrors)
	validationError := validationErrors[0]

	switch validationError.Tag() {
	case "required":
		return errors.New(validationError.StructField() + " is required")
	case "max":
		return errors.New(validationError.StructField() + " is required with max " + validationError.Param())
	case "min":
		return errors.New(validationError.StructField() + " is required with min " + validationError.Param())
	case "email":
		return errors.New(validationError.StructField() + " must be a valid email")
	}
	return nil
}
