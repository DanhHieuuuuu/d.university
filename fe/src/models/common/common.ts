export const genderE: Record<string, number> = {
  male: 1,
  female: 2,
  other: 3
};

export const userStatusE: Record<string, number> = {
  pending: 1,
  active: 2,
  disabled: 3
};

export enum PermisionActionE {
  READ = 'read',
  CREATE = 'create',
  UPDATE = 'update',
  DELETE = 'delete'
}

export enum EnvironmentE {
  DEVELOPMENT = 'development',
  PRODUCTION = 'production'
}

export enum EStatusResonse {
  SUCCESS = 1,
  ERROR = 0
}
