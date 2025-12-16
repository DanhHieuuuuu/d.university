import dayjs from 'dayjs';

export const buildCreateFormData = (values: Record<string, any>): FormData => {
  const formData = new FormData();

  Object.entries(values).forEach(([key, value]) => {
    if (value === undefined || value === null) return;

    // DatePicker (dayjs)
    if (value?.$d) {
      formData.append(key, dayjs(value).format('YYYY-MM-DD'));
      return;
    }

    // File upload (DetailDelegation)
    if (key === 'DetailDelegation' && value instanceof File) {
      formData.append('DetailDelegation', value);
      return;
    }

    formData.append(key, value.toString());
  });

  return formData;
};
