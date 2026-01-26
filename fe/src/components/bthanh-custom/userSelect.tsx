import { Select } from 'antd';

export type UserOption = {
  value: number;
  label: string;
  searchText: string;
};

type UserSelectProps = {
  options: UserOption[];
  value?: UserOption;
  onChange?: (v?: UserOption) => void;
  disabled?: boolean;
  loading?: boolean;
  placeholder?: string;
};

const UserSelect: React.FC<UserSelectProps> = ({
  options,
  value,
  onChange,
  loading,
  disabled,
  placeholder = 'Chọn nhân sự'
}) => {
  return (
    <Select
      showSearch
      labelInValue
      allowClear
      options={options}
      value={value}
      loading={loading}
      disabled={disabled}
      placeholder={placeholder}
      optionFilterProp="searchText"
      filterOption={(input, option) => (option?.searchText ?? '').toLowerCase().includes(input.toLowerCase())}
      onChange={onChange}
    />
  );
};

export default UserSelect;
