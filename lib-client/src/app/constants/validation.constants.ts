export const ValidationConstants = {
  author: {
    name: {
      minLength: 2,
      maxLength: 50,
      pattern: /^[a-zA-Z\s\-']+$/
    },
    surname: {
      minLength: 2,
      maxLength: 50,
      pattern: /^[a-zA-Z\s\-']+$/
    },
    country: {
      minLength: 2,
      maxLength: 60
    },
    birthday: {
      minYear: 1900
    }
  }
};

export const ErrorMessages = {
  author: {
    name: {
      required: 'Name is required',
      minLength: 'Name must be at least 2 characters long',
      maxLength: 'Name cannot exceed 50 characters',
      pattern: 'Name can only contain letters, spaces, hyphens and apostrophes'
    },
    surname: {
      required: 'Surname is required',
      minLength: 'Surname must be at least 2 characters long',
      maxLength: 'Surname cannot exceed 50 characters',
      pattern: 'Surname can only contain letters, spaces, hyphens and apostrophes'
    },
    country: {
      required: 'Country is required',
      minLength: 'Country name must be at least 2 characters long',
      maxLength: 'Country name cannot exceed 60 characters'
    },
    birthday: {
      required: 'Birthday is required',
      past: 'Birthday must be in the past',
      minYear: 'Birthday must be after 1900'
    }
  }
}; 