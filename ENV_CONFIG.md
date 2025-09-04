# Environment Variables Configuration

## Required Environment Variables for Production

### JWT Configuration
- `JWT_SECRET_KEY`: JWT signing key (should be at least 32 characters long)
  - Example: `openssl rand -base64 32` to generate a secure key

### CORS Configuration (optional)
The application reads CORS allowed origins from `appsettings.json` under `Cors:AllowedOrigins`.

## Development Setup

For development, you can use the values in `appsettings.Development.json`. 

**Never use development secrets in production!**

## Example Environment Variables

```bash
# Production environment
export JWT_SECRET_KEY="your-very-secure-jwt-secret-key-here"
```

## Docker Configuration

If using Docker, add these to your `docker-compose.yml`:

```yaml
environment:
  - JWT_SECRET_KEY=your-very-secure-jwt-secret-key-here
```

## Health Checks

The application includes health checks available at:
- `/health` - Basic health check endpoint