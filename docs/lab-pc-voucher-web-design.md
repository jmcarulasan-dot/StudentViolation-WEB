# MudBlazor Lab Staff Console

The LabStaff web area and MFA-aware login are implemented on this branch.

## Role boundaries

LabStaff has a separate web route for PC registration, voucher issue/revoke, and session review/end. SAO provisions distinct LabStaff accounts; those accounts do not gain SAO or Admissions access. Admissions and SAO remain separate. Guard and Student workflows remain mobile-only. No Guidance role is added.

API authorization remains authoritative; the web route also checks the signed-in role. One-time enrollment codes, vouchers, and initial LabStaff passwords are shown only after creation and are not stored for later display.

## Implementation areas

- MFA response handling: `AuthService`, `LoginResponse`, and `Login.razor`.
- LabStaff console: `Pages/LabStaff.razor` with typed services and models.
- SAO provisioning: `Pages/LabStaffAccounts.razor` and the SAO dashboard link.

Before staff use, apply the reviewed lab-PC SQL migration, configure the API address for HTTPS, and confirm the LabStaff route and management actions against the API.