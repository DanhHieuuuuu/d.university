import type { IQueryPaging } from '@models/common/model.common';

export type IModelChatbot = {
    id?: number;
    name?: string;
    description?: string;
    baseURL?: string;
    modelName?: string;
    isLocal?: boolean;
    isSelected?: boolean;
};

export type ICreateModelChatbot = {
    name?: string;
    description?: string;
    baseURL?: string;
    apiKey?: string;
    modelName?: string;
    isLocal?: boolean;
    isSelected?: boolean;
};

export type IQueryModelChatbot = IQueryPaging & {
    Name?: string;
};

export type IUpdateModelChatbot = ICreateModelChatbot & {
    id: number | null;
};