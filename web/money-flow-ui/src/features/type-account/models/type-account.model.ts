export interface TypeAccountView {
    id: string,
    name: string
}

export interface CreateTypeAccountRequest {
    name: string
}

export interface UpdateTypeAccountRequest {
    id: string,
    name: string
}

export interface CreatedTypeAccountResponse {
    id: string
}

export interface TypeAccountResponse {
    id: string,
    name: string
}