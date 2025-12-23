import { Input, Form, Typography } from "antd";
import type { Rule } from "antd/es/form";

// Định nghĩa kiểu dữ liệu props
interface InputAntdWithTitleProps {
  title?: string;
  name: string;
  label?: string;
  placeholder?: string;
  rules?: Rule[];
  type?: string;
  disabled?: boolean;
  allowClear?: boolean;
}

const InputAntdWithTitle: React.FC<InputAntdWithTitleProps> = ({
  title,
  name,
  placeholder,
  rules = [],
  type = "text",
  disabled = false,
  allowClear = true,
}) => {
  return (
    <div style={{ marginBottom: 16 }}>
      <p>{ title }</p>

      <Form.Item name={name} rules={rules} style={{ marginBottom: 0 }}>
        <Input
          type={type}
          placeholder={placeholder}
          disabled={disabled}
          allowClear={allowClear}
        />
      </Form.Item>
    </div>
  );
};

export default InputAntdWithTitle;
