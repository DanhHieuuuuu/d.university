import { removeVietnameseTones } from "@helpers/format.helper";
import { AutoComplete, Form } from "antd";

interface OptionType {
    value: string;
    label?: string;
}

interface AutoCompleteAntdProps {
    name: string | (string | number)[];
    title?: string;
    placeholder?: string;
    options: OptionType[];
    allowClear?: boolean;
}

const AutoCompleteAntd: React.FC<AutoCompleteAntdProps> = ({
    title,
    name,
    placeholder,
    options,
    allowClear = true,
}) => {
    return (
        <div style={{ marginBottom: 16 }}>
            <p>{title}</p>
            <Form.Item name={name}>
                <AutoComplete
                    placeholder={placeholder}
                    allowClear={allowClear}
                    options={options}
                    filterOption={(inputValue, option) =>
                        removeVietnameseTones(option?.value || "").includes(
                            removeVietnameseTones(inputValue)
                        )
                    }
                    style={{ width: "100%" }}
                />
            </Form.Item>
        </div>
    );
};

export default AutoCompleteAntd;
